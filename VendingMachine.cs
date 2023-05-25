using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineApp
{
    internal class VendingMachine
    {
        public List<VendingMachineItem> Items { get; private set; }

        public VendingMachine(List<VendingMachineItem> items)
        {
            Items = items;
        }

        public VendingMachineItem GetItemByName(string name)
        {
            foreach (var item in Items)
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    return item;
                }
            }
            return null;

        }
        public bool IsItemInStock(VendingMachineItem item, int quantity = 0)
        {
            return item.Stock >= quantity;
        }

        public void ReduceStock(VendingMachineItem item, int change)
        {
            item.Stock += change;
        }

        public VendingMachineItem GetItemByIndex(int index)
        {
            if (index >= 0 && index < Items.Count)
            {
                return Items[index];
            }
            return null;
            }
        }
    }
