using Xunit;
using System.Collections.Generic;
using GildedRoseKata;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using Xunit.Abstractions;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Fact]
        public void updateQuality_should_not_change_the_items_name()
        {
            string itemNameTest = "foo";
            IList<Item> items = new List<Item> { new Item { Name = itemNameTest, SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(itemNameTest, items[0].Name);
        }

        [Fact]
        public void once_the_sell_by_date_has_passed_Quality_degrades_twice_as_fast()
        {
            IList<Item> items = new List<Item> { new Item { Name = "", SellIn = 0, Quality = 2 } };
            GildedRose app = new GildedRose(items);
            app.UpdateSellIn();
            app.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }
    
        [Fact]
        public void the_quality_of_an_item_is_never_negative()
        {
            IList<Item> items = new List<Item> { new Item { Name = "", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }

        [Fact]
        public void agedBrie_actually_increases_in_Quality_the_older_it_gets()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 1, Quality = 1 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(2, items[0].Quality);
        }

        [Fact]
        public void the_Quality_of_an_item_is_never_more_than_50()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 1, Quality = 50 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(50, items[0].Quality);
        }

        [Fact]
        public void sulfuras_never_has_to_decreases_in_quality()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 1, Quality = 80 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(80, items[0].Quality);
        }

        [Fact]
        public void backstage_passes_actually_increases_in_Quality_the_older_it_gets()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 10 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(11, items[0].Quality);
        }

        [Fact]
        public void backstage_passes_quality_increases_by_2_when_there_are_10_days_or_less()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 48 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(50, items[0].Quality);
        }

        [Fact]
        public void backstage_passes_quality_increases_by_3_when_there_are_5_days_or_less()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 47 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(50, items[0].Quality);
        }

        [Fact]
        public void backstage_passes_drops_to_0_after_the_concert()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 47 } };
            GildedRose app = new GildedRose(items);
            app.UpdateSellIn();
            app.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
        }

        [Fact]
        public void conjured_items_degrade_in_quality_twice_as_fast_as_normal_items()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 } };
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(4, items[0].Quality);
        }
    }
}
