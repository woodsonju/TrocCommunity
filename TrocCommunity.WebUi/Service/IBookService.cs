using System.Collections.Generic;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.WebUi.Service
{
    public interface IBookService
    {
        Task<List<string>> GetAuthors(string isbn);

        Task<double> GetAverageRatingCritical(string isbn);

        Task<List<string>> GetCategories(string isbn);

        Task<string> GetDateEdition(string isbn);

       Task<string> GetDescription(string isbn);

        Task<string> GetDimension(string isbn);

        Task<string> GetEdithor(string isbn);

        Task<string> GetImage(string isbn);

        Task<string> GetLanguage(string isbn);

        Task<string> GetTitle(string isbn);

        Task<int> GetVolume(string isbn);

        Task<double> GetPrice(string isbn);

        double GetPoints(double price, EtatDuLivre etatDuLivre, bool isExchange);
        double GetPoints(double price, EtatDuLivre etatDuLivre);


    }
}