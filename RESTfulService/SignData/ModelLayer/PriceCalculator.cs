using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignData.ModelLayer
{
    internal class PriceCalculator : IPriceCalculator
    {
        public double calculatePrice(double pricePerMinute, int duration)
        {
            double price= 0;

            if(duration > 0 && pricePerMinute > 0)
            {
                price = pricePerMinute * duration;
            }

            return price;
        }
    }
}
