using Apsiyon.Entities;

namespace Apsiyon.Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Added product";
        public static string ProductDeleted = "Deleted product";
        public static string ProductUpdated = "Updated product";

        public static string CategoryAdded = "Added category";
        public static string CategoryUpdated = "Updated category";
        public static string CategoryDeleted = "Deleted category";

        public static string UserNotFound = "User not found";
        public static string PasswordError = "Username or password incorrect";
        public static string SuccessFullLogin = "Login successful";

        public static string UserAlreadyExists = "I already have the e-mail information of this e-mail address on my site.!";
        public static string UserRegistered = "User successfully registered to the system.";
        public static string AccessTokenCreated = "Token created";

        public static string ErrorProductAdded = "The product with this product name already exists in the system";
        public static string ErrorCategoryAdded = "There are enough products in this category !";

        public static string NotFoundProduct = "No product with id found !";
        public static string NotFoundCategory = "Category with id not found !";

        public static string EmptyObject = $"Please make sure to send {typeof(GeneralFilter)} object to filter!";
    }
}
