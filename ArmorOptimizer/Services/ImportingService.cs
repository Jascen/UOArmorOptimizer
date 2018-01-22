using ArmorOptimizer.Core.Attributes;
using ArmorOptimizer.Core.Enums;
using ArmorOptimizer.Core.Extensions;
using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Models;
using ArmorOptimizer.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArmorOptimizer.Services
{
    public class ImportingService
    {
        protected readonly DatabaseService DatabaseService;
        protected readonly DataImportViewModel Model;

        public ImportingService(DataImportViewModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            DatabaseService = new DatabaseService();
            ImportFileCommand = new DelegateCommand(async () => await ImportAsync(), CanImportFile);
        }

        #region Commands

        public DelegateCommand ImportFileCommand { get; }

        #endregion Commands

        public bool CanImportFile()
        {
            //TODO: Figure out why this isn't being refreshed
            return true;
            //return !string.IsNullOrWhiteSpace(Model.SelectedFilepath);
        }

        public async Task ImportAsync()
        {
            if (!CanImportFile()) throw new InvalidOperationException("Cannot bypass guard.");

            var easyUoRecords = new List<EasyUoRecord>();
            var keyMap = BuildKeyMap();
            var text = await ReadTextAsync(Model.SelectedFilepath);
            var lines = Regex.Split(text, "\r\n|\r|\n");

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var splitLine = line.Split('.');
                if (splitLine.Length == 0) continue;

                var uoRecord = new EasyUoRecord();
                uoRecord.Id = splitLine[keyMap.Id];
                uoRecord.Slot = int.Parse(splitLine[keyMap.Slot]);
                uoRecord.ItemType = splitLine[keyMap.ItemType];
                uoRecord.Color = int.Parse(splitLine[keyMap.Color]);
                uoRecord.Physical = int.Parse(splitLine[keyMap.Physical]);
                uoRecord.Fire = int.Parse(splitLine[keyMap.Fire]);
                uoRecord.Cold = int.Parse(splitLine[keyMap.Cold]);
                uoRecord.Poison = int.Parse(splitLine[keyMap.Poison]);
                uoRecord.Energy = int.Parse(splitLine[keyMap.Energy]);
                easyUoRecords.Add(uoRecord);
            }

            var items = CreateItems(easyUoRecords);
            await DatabaseService.AddItemsAsync(items);
        }

        protected EasyUoRecordKeyMap BuildKeyMap()
        {
            var keyMap = new EasyUoRecordKeyMap();
            var keyMapType = keyMap.GetType();

            var recordProperties = typeof(EasyUoRecord).GetProperties();
            var mapProperties = typeof(EasyUoRecordKeyMap).GetProperties();
            foreach (var mapProperty in mapProperties)
            {
                var recordProperty = recordProperties.FirstOrDefault(r => r.Name == mapProperty.Name);
                if (recordProperty == null) throw new ArgumentException($"Unable to find '{mapProperty.Name}' on record key map.");

                var attributes = recordProperty.GetCustomAttributes(true);
                var columnNumber = attributes.FirstOrDefault(a => a is ColumnNumber) as ColumnNumber;
                if (columnNumber == null) throw new ArgumentException($"Property '{recordProperty.Name}' did not have column number assigned.");

                keyMapType.InvokeMember(mapProperty.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, Type.DefaultBinder, keyMap, new object[] { columnNumber.Value });
            }

            return keyMap;
        }

        private IEnumerable<Item> CreateItems(IEnumerable<EasyUoRecord> records)
        {
            var items = new List<Item>();
            var knownArmorTypes = Model.ArmorTypes.ToEnumeratedList();
            var knownResources = Model.Resources.ToEnumeratedList();
            foreach (var record in records)
            {
                var item = new Item
                {
                    PhysicalResist = record.Physical,
                    FireResist = record.Fire,
                    ColdResist = record.Cold,
                    PoisonResist = record.Poison,
                    EnergyResist = record.Energy,
                    UoId = record.Id,

                    // TBD???
                    LostResistancePoints = 0,
                    NumberOfImbues = 0,
                };

                var armorType = knownArmorTypes.FirstOrDefault(a => a.ItemType == record.ItemType);
                if (armorType == null)
                {
                    armorType = new ArmorType
                    {
                        Name = $"Autocreated - {record.ItemType}",
                        ItemType = record.ItemType,
                        BaseResistId = -1,
                        BaseResourceKindId = -1,
                        SlotId = (int)SlotTypes.Unknown,
                    };
                    //TODO: EF call to add it here?  Will it add duplicates at the end?
                    knownArmorTypes.Add(armorType);
                }

                item.ArmorType = armorType;

                // TODO: This is fragile if Servers have the same color on different resources
                var resource = knownResources.FirstOrDefault(r => r.Color == record.Color && r.BaseResourceKindId == item.ArmorType.BaseResourceKindId);
                if (resource == null)
                {
                    resource = new Resource
                    {
                        Name = $"Autocreated - {record.ItemType}",
                        BaseResourceKindId = -1,
                        BonusResistId = -1,
                        Color = record.Color,
                    };
                    //TODO: EF call to add it here?  Will it add duplicates at the end?
                    knownResources.Add(resource);
                }

                item.Resource = resource;

                if (armorType.Id > 0)
                {
                    item.ArmorTypeId = armorType.Id;
                    item.ArmorType = null;
                }

                if (resource.Id > 0)
                {
                    item.ResourceId = resource.Id;
                    item.Resource = null;
                }

                items.Add(item);
            }

            Model.Resources = knownResources;
            Model.ArmorTypes = knownArmorTypes;

            return items;
        }

        private async Task<string> ReadTextAsync(string filePath)
        {
            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                var sb = new StringBuilder();

                var buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    var text = Encoding.UTF8.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }
    }
}