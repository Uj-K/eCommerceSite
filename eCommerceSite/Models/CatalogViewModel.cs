namespace eCommerceSite.Models
{
    public class CatalogViewModel
    {
        public CatalogViewModel(List<MusicInstrument> musicInstruments, int lastPage, int currPage)
        {
            MusicInstruments = musicInstruments;
            LastPage = lastPage;
            CurrentPage = currPage;
        }

        public List<MusicInstrument> MusicInstruments { get; private set; }

        /// <summary>
        /// The last page of the catalog. 
        /// Calculated by having a total number of products
        /// divided by products per page
        /// </summary>
        public int LastPage { get; private set; }

        /// <summary>
        /// The current page the user is viewing
        /// </summary>
        public int CurrentPage { get; private set; }

        // private 으로 set 해준 이유는 데이터를 받아서 건내주기만 하면 되고 수정할 필요가 없기 때문이다.
        // so they can be set in the class but nowhere else.
    }
}
