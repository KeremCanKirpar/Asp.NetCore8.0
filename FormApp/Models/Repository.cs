namespace FormApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();

        static Repository()
        {
            _products.Add(new Product { Id = 1, Name = "Iphone 14", Price = 40000, Image = "1.jpg", IsActive = false, CategoryId = 1 });
            _products.Add(new Product { Id = 2, Name = "Iphone 15", Price = 50000, Image = "2.jpg", IsActive = false, CategoryId = 1 });
            _products.Add(new Product { Id = 3, Name = "Iphone 16", Price = 60000, Image = "3.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { Id = 4, Name = "Macbook Air", Price = 80000, Image = "5.jpg", IsActive = false, CategoryId = 2 });
            _products.Add(new Product { Id = 5, Name = "Macbook Pro", Price = 90000, Image = "6.jpg", IsActive = true, CategoryId = 2 });

            _categories.Add(new Category { CategoryId = 1, CategoryName = "Telefon" });
            _categories.Add(new Category { CategoryId = 2, CategoryName = "Bilgisayar" });
        }
        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }

        public static void EditProduct(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if(entity != null)
            {
                if(!string.IsNullOrEmpty(updatedProduct.Name))
                {
                    entity.Name = updatedProduct.Name;
                }
                entity.Name = updatedProduct.Name;
                entity.Price = updatedProduct.Price;
                entity.Image = updatedProduct.Image;
                entity.IsActive = updatedProduct.IsActive;
                entity.CategoryId = updatedProduct.CategoryId;
            }
        }
        public static void EditIsActive(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (entity != null)
            {
                entity.IsActive = updatedProduct.IsActive;
            }
        }
        public static void DeleteProduct(Product deletedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.Id == deletedProduct.Id);
            if (entity != null)
            {
                _products.Remove(entity);
            }
        }
        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
    }
}