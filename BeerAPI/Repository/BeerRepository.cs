using BeerAPI.Interfaces;
using BeerAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeerAPI.Repository
{
    public class BeerRepository : IBeerRepository
    {
        private List<Beer> _beers;
        //TODO shiko menyra se si ta besh shtimin ne memorie
        public BeerRepository()
        {
            _beers = new List<Beer>()
            {
                new Beer
                {
                    ID = 1,
                    Name = "Paulanher",
                    Rating = 4,
                    Type = "Pale ale"
                },
                new Beer
                {
                    ID = 2,
                    Name = "Heineken",
                    Rating = 3,
                    Type = "Pale ale"
                }
            };
        }

        public IEnumerable<Beer> GetBeers()
        {
            return _beers;
        }

        public IEnumerable<Beer> FilterBeers(string filter)
        {
            return _beers.Where(b => string.IsNullOrWhiteSpace(filter) || b.Name.ToLower().Contains(filter.ToLower()));
        }

        public Beer AddBeer(Beer beer)
        {
            _beers.Add(beer);
            return beer;
        }

        public Beer UpdateBeer(UpdateBeer model)
        {
            var beer = _beers.FirstOrDefault(b => b.ID == model.ID);

            if (beer is null)
                throw new KeyNotFoundException("Beer not found!");

            var rating = ((beer.Rating is null ? 0 : beer.Rating.Value) + model.Rating) / 2;

            beer.Rating = rating;

            _beers.Remove(beer);
            _beers.Add(beer);

            return beer;
        }
    }
}