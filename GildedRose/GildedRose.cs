using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        private const string aged_Brie_Name = "Aged Brie";
        private const string backstage_Name = "Backstage passes to a TAFKAL80ETC concert";
        private const string sulfuras_Name = "Sulfuras, Hand of Ragnaros";
        private const int max_Quality = 50;
        private const string conjured_beginning_Name = "Conjured";
        private const int min_Quality = 0;
        private const int firstSellInStepBacktageQualityUpgrade = 11;
        private const int secondSellInStepBacktageQualityUpgrade = 6;
        private IList<Item> Items;
        
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        /// <summary>
        /// Update Items SellIn 
        /// </summary>
        public void UpdateSellIn()
        {
            for (var indexItem = 0; indexItem < Items.Count; indexItem++)
            {        
                if (Items[indexItem].Name != sulfuras_Name)
                {
                    Items[indexItem].SellIn = Items[indexItem].SellIn - 1;
                }
            }
        }
        
        /// <summary>
        /// Update Items Quality 
        /// </summary>
        public void UpdateQuality()
        {
            int qualityUpdate;

            for (var indexItem = 0; indexItem < Items.Count; indexItem++)
            {
                qualityUpdate = 0;
                Item currentItem = Items[indexItem];
                bool isQualityUnderMaxQuality = currentItem.Quality < max_Quality;
                bool isQualityUnderMinQuality = currentItem.Quality > min_Quality;
                bool isItemBackStage = currentItem.Name == backstage_Name;
                bool isItemAged_Brie = currentItem.Name == aged_Brie_Name;
                bool isItemNotSulfuras = currentItem.Name != sulfuras_Name;

                if (isItemAged_Brie || isItemBackStage)
                {
                    if (isQualityUnderMaxQuality)
                    {
                        qualityUpdate += 1;

                        if (isItemBackStage && isQualityUnderMaxQuality)
                        {
                            if (currentItem.SellIn < secondSellInStepBacktageQualityUpgrade)
                            {
                                qualityUpdate += 2;
                            }
                            else
                            {
                                if (currentItem.SellIn < firstSellInStepBacktageQualityUpgrade)
                                {
                                    qualityUpdate += 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (isQualityUnderMinQuality && isItemNotSulfuras)
                    {
                        qualityUpdate = currentItem.Name.StartsWith(conjured_beginning_Name) ? (qualityUpdate - 2) : (qualityUpdate - 1);
                    }
                }

                if (currentItem.SellIn < 0)
                {
                    if (isItemAged_Brie && isQualityUnderMaxQuality)
                    {
                        qualityUpdate += 1;
                    }
                    else
                    {
                        if (isItemBackStage)
                        {
                            qualityUpdate = -currentItem.Quality;
                        }
                        else
                        {
                            if (isQualityUnderMinQuality && isItemNotSulfuras)
                            {
                                qualityUpdate -= 1;
                            }
                        }
                    }
                }

                currentItem.Quality = currentItem.Quality + qualityUpdate;

            }
        }
    }
}
