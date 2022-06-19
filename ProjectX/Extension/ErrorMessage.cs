namespace ProjectX.Extension
{
    public static class ErrorMessage
    {
        public static string ProductCreateDtoNameMaxLength = "Please enter less than 100 caracter!";
        public static string CategoryAlredyExist = "This Category alredy exist!";
        public static string ImageFormat = "Please upoad only image format!";
        public static string PriceMoreThanZero = "Price should be more than 0!";

        public static string NotNull(string prop)
        {
            return $"{prop} sholdn't be null!";
        }
        public static string InValidSize(int size)
        {
            return $"Image size more than {size}MB!";
        }
    }
}
