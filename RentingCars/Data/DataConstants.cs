namespace RentingCars.Data
{
    public class DataConstants
    {
        public class Car
        {
            public const int CarBrandMinLength = 2;
            public const int CarBrandMaxLength = 20;

            public const int CarModelMinLength = 2;
            public const int CarModelMaxLength = 30;

            public const int CarDescriptionMinLength = 6;
            public const int CarDescriptionMaxLength = 160;

            public const int CarYearMinValue = 2000;
            public const int CarYearMaxValue = 2100;
        }

        public class Category
        {
            public const int CategoryNameMaxLength = 25;
        }

        public class Dealer
        {
            public const int DealerNameMaxLength = 30;
            public const int DealerPhoneNumberMaxLength = 30;
        }
    }
}
