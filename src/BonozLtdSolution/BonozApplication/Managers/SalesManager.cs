//namespace BonozApplication.Managers
//{
//    public class SalesManager : BaseDataManager, ISales
//    {
//        public SalesManager(BanazDbContext model) : base(model)
//        {
//        }


//        #region Category

//        public int CreateCategory(ProductCategory category)
//        {
//            AddUpdateEntity(category);
//            return category.Id;
//        }

//        public bool UpdateCategory(ProductCategory category)
//        {
//            return AddUpdateEntity(category);
//        }

//        public ProductCategory GetCategory(int id)
//        {
//            return FindEntity<ProductCategory>(id);
//        }

//        //public IList<PetitionListDTO> GetAllPetitionsList(int caseId)
//        //{
//        //    return GetListData<PetitionListDTO>($"EXEC GetAllPetitions_ByCaseId @caseId={caseId}");
//        //}

//        public bool DeleteCategory(int id)
//        {
//            return RemoveEntity<ProductCategory>(id);
//        }

//        public IList<ProductCategory> GetAllCategories()
//        {
//            return GetEntityListData<ProductCategory>();
//        }

//        //private async Task SetUsers(HttpResponseMessage result)
//        //{
//        //    var respons = await result.Content.ReadFromJsonAsync<List<AppUser>>();
//        //    AppUsers = respons;
//        //    _navigationManager.NavigateTo("AppUsers");

//        //}

//        #endregion Category
//    }
//}
