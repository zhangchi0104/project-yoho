using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.libs
{
    public enum BrandList
    {
        AmericanExpress = 1
    }
    class TrieRep
    {
        private TrieNode[] root = new TrieNode[10];
        public TrieRep()
        {
            for(int i =0; i < 10; i++)
            {
                root[i] = null;
            }
            this.initializeBrands();
        }
        private void insertBrand(string number, string brand)
        {
            int hi = number.Length;
            int head =(int)Char.GetNumericValue(number[0]);
            this.root[head] = doInsertBrand(this.root[head], brand, number, 0, hi - 1);
        }
        private  TrieNode doInsertBrand(TrieNode  t, string  brand, string prefixNum, int lo,  int hi)
        {
            if (lo > hi) { return t; }
            if (t==null)
            {
                if (lo < hi)
                {
                    t = new TrieNode(null);
                    int nD = (int)Char.GetNumericValue(prefixNum[lo + 1]);
                    t.NextDigit[nD] = doInsertBrand(t.NextDigit[nD], brand, prefixNum, lo + 1, hi);
                } else
                {
                    t = new TrieNode(brand);
                }
                return t;
            } else
            {
                if (lo == hi) { t.Brand = brand; }
                else
                {
                    int nD = (int)Char.GetNumericValue(prefixNum[lo + 1]);
                    t.NextDigit[nD] = doInsertBrand(t.NextDigit[nD], brand, prefixNum, lo + 1, hi);
                }
                return t;
            } 
        }
        public string GetBrand(string number)
        {
            int len = number.Length;
            int cD = 0;
            TrieNode curr = null;
            cD = (int)Char.GetNumericValue(number[0]);
            curr = this.root[cD];
            
            
            string brand = "Unknown";
            for (int i = 0; i < len;)
            {
                if (curr == null)
                {
                    break;
                } else
                {
                    if (curr.Brand != null) { brand = curr.Brand; }
                    i++;
                    if (i == len) { break;}
                    int nD = (int)Char.GetNumericValue(number[i]);
                    curr = curr.NextDigit[nD];
                }
            }
            return brand;
        }
        private void initializeBrands()
        {
            this.insertBrand("34", "American Express");
            this.insertBrand("37", "American Express");
            this.insertBrand("31", "China T-Union");
            this.insertBrand("62", "China Union Pay");

            for(int i = 300; i <= 305; i++)
            {
                string num = Convert.ToString(i);
                this.insertBrand(num, "Diners Club International");
            }

            this.insertBrand("3095", "Diners Club International");
            this.insertBrand("38", "Diners Club International");
            this.insertBrand("39", "Diners Club International");

            this.insertBrand("6011", "Discover Card");
            this.insertBrand("64", "Discover Card");
            this.insertBrand("65", "Discover Card");

            this.insertBrand("60", "RuPay");
            this.insertBrand("6521", "RuPay");

            this.insertBrand("636", "InterPayment");
            
            for (int i = 3258; i <= 3589; i++)
            {
                string num = Convert.ToString(i);
                this.insertBrand(num, "JCB");
            }

            this.insertBrand("50", "Maestro");
            this.insertBrand("56", "Maestro");
            this.insertBrand("57", "Maestro");
            this.insertBrand("58", "Maestro");
            this.insertBrand("639", "Maestro");
            this.insertBrand("67", "Maestro");

            this.insertBrand("5019", "Dankort");

            this.insertBrand("2200", "MIR");
            this.insertBrand("2201", "MIR");
            this.insertBrand("2202", "MIR");
            this.insertBrand("2203", "MIR");
            this.insertBrand("2204", "MIR");

            for (int i = 222100; i<= 272099; i++)
            {
                string num = Convert.ToString(i);
                this.insertBrand(num, "MasterCard");
            }

            for (int i = 51; i <=55; i++)
            {
                string num = Convert.ToString(i);
                this.insertBrand(num, "MasterCard");
            }

            for (int i = 979200; i <= 979289; i++)
            {
                string num = Convert.ToString(i);
                this.insertBrand(num, "Troy");
            }

            this.insertBrand("4", "visa");
            this.insertBrand("1", "UATP");

            for (int i =506099; i <= 506198; i++)
            {
                string num = Convert.ToString(i);
                this.insertBrand(num, "Verve");
            }

            for (int i = 650002; i <= 650027; i++)
            {
                string num = Convert.ToString(i);
                this.insertBrand(num, "Verve");
            }
        }
    }
    class TrieNode
    {
        public string Brand;
        public TrieNode[] NextDigit  = new TrieNode[10];

        public TrieNode(string brand)
        {
            Brand = brand;
            for (int i = 0; i < 10; i++)
            {
                NextDigit[i] = null;
            }
        }
    }

}
