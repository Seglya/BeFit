namespace BeFit.Models.UserProfileViewModels
{
    public class WeightOfFoodViewModel
    {
       
        public double Weight { get; set; } //вес продукта
        public int FoodID { get; set; } //ID продукта
        public int IngestionId { get; set; }
        public Food Food { get; set; }

        public double Calories
        {
            get
            {
                if (Food == null) return 0;
                return Food.Calories * Weight / 100;
            }
        } //Каллорийность блюда
        public double Fat {get{
            if (Food == null)
            {
                return 0;
            }
            return Food.Fat * Weight/100;}}  // Количество жира
        public double Protein {get{ if (Food == null) return 0; return Food.Protein * Weight/100;} } //Количество белка

        public double Carbohydrate
        {
            get
            {
                if (Food == null) return 0;
                return Food.Carbohydrate * Weight/100;
            }
        } //Количество углеводов
    }
}
