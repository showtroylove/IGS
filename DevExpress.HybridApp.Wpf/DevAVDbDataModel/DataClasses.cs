using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;

namespace DevExpress.DevAV.DevAVDbDataModel {

    public class HomeWithPhoto 
    {
        public Home Home { get; set; }
        public byte[] Image { get; set; }
    }

    [XmlType("Homes")]
    public class Home {
        private const int PhotosCount = 7;
        private const int AgentsCount = 6;
        private const int LayoutsCount = 5;

        [Key]
        public int ID { get; set; }
        public string Address { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public double HouseSize { get; set; }
        public double LotSize { get; set; }
        public decimal Price { get; set; }
        public string Features { get; set; }
        public string YearBuilt { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public byte[] Photo { get; set; }
        [XmlIgnore]
        public int HomePhotoID => ID % PhotosCount + 1;

        [XmlIgnore]
        public int AgentID => ID % AgentsCount + 1;

        [XmlIgnore]
        public int HomeLayoutID => ID % LayoutsCount;

        [XmlIgnore]
        public int StatisticDataID => ID - 1;

        [XmlIgnore]
        public HomePriceStatisticDataKey HomePriceStatisticDataKey => new HomePriceStatisticDataKey { ID = ID - 1, Price = Price };
    }
    [XmlType("Homes")]
    public class HomePhoto {
        [Key]
        public int ID { get; set; }
        public int ParentID { get; set; }
        public byte[] Photo { get; set; }
    }
    [XmlType("Agents")]
    public class Agent {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public byte[] Photo { get; set; }
        public string Email { get; set; }
        [XmlIgnore]
        public int AgentStatisticDataID => ID - 1;
    }
    public class HomeLayout {
        [Key]
        public int ID { get; set; }
        public byte[] Image { get; set; }
    }
    public class MortgageRate {
        [Key]
        public DateTime Date { get; set; }
        public double FRM30 { get; set; }
        public double FRM15 { get; set; }
        public double ARM1 { get; set; }
    }
    public class HomePopularityRatingPoint {
        public string Region { get; set; }
        public int Value { get; set; }
    }
    public class HomePopularityRating {
        [Key]
        public int ID { get; set; }
        public IQueryable<HomePopularityRatingPoint> Points { get; set; }
    }
    public class HomePriceStatisticDataPoint {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
    public struct HomePriceStatisticDataKey {
        public int ID { get; set; }
        public decimal Price { get; set; }
    }
    public class HomePriceStatisticData {
        [Key]
        public int ID { get; set; }
        public HomePriceStatisticDataKey Key { get; set; }
        public IQueryable<HomePriceStatisticDataPoint> Points { get; set; }
    }
    public class SimilarHousesStatisticDataPoint {
        public int Year { get; set; }
        public int ProposalCount { get; set; }
        public int SoldCount { get; set; }
    }
    public class SimilarHousesStatisticData {
        [Key]
        public int ID { get; set; }
        public IQueryable<SimilarHousesStatisticDataPoint> Points { get; set; }
    }
    public class AgentStatisticDataPoint {
        public string Region { get; set; }
        public int Year { get; set; }
        public int Value { get; set; }
    }
    public class AgentStatisticData {
        [Key]
        public int ID { get; set; }
        public IQueryable<AgentStatisticDataPoint> Points { get; set; }
    }
}
