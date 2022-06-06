using WebApi.Interfaces;
using WebApi.Data;

namespace WebApi.Repository
{
    public class SuperHeroRepository : ISuperHeroRepository
    {
        private readonly DataContext Context;
        public SuperHeroRepository(DataContext Context)
        {
            this.Context = Context;
        }

        /****** Generic Interface Methods *****/
        /*************************************/

        public List<SuperHero> Read()
        {
            return this.Context.SuperHeroes.ToList();
        }

        public SuperHero Update(SuperHero sp)
        {
            SuperHero superHero = this.Context.SuperHeroes.Find(sp.Id);
            if(superHero != null)
            {
                if(sp.FirstName.Equals("") != true)
                    superHero.FirstName = sp.FirstName;
                
                if(sp.HeroName.Equals("") != true)
                    superHero.HeroName = sp.HeroName;

                if(sp.LastName.Equals("") != true)
                    superHero.LastName = sp.LastName;

                if(sp.Universe.Equals("") != true)
                    superHero.Universe = sp.Universe;
                
                if(sp.Picture.Equals("") != true)
                    superHero.Picture = sp.Picture;
                
                this.Context.SaveChanges();
                return sp;
            }

            return null;
        }

        public SuperHero Delete(int id)
        {
            SuperHero sp = this.Context.SuperHeroes.Find(id);
            if(sp == null)
            {
                return null;
            }

            this.Context.SuperHeroes.Remove(sp);
            this.Context.SaveChanges();
            return sp;
        }

        public SuperHero Create(SuperHero sp)
        {
            List<SuperHero> ListOfHeroInDataBase = this.Context.SuperHeroes.ToList();

            foreach (var item in ListOfHeroInDataBase)
            {
                if(sp.HeroName.ToLower() == item.HeroName.ToLower())
                {
                    return null;
                }
            }

            this.Context.SuperHeroes.Add(sp);
            this.Context.SaveChanges();
            return sp;
        }

        public SuperHero ReadOne(int id)
        {
            SuperHero sp = this.Context.SuperHeroes.Find(id);
            if(sp != null)
            {
                return sp;
            }

            return null;
        }

        /****** Sp Interface Methods *****/
        /*********************************/

        public List<SuperHero> GetAllInformation(string info)
        {
            info = info.ToLower();
            List<SuperHero> ListOfHeroInDataBase = this.Context.SuperHeroes.ToList();
            List<SuperHero> listOfHeroFind = new List<SuperHero>();

            foreach (var item in ListOfHeroInDataBase)
            {
                if(item.FirstName.ToLower() == info || item.LastName.ToLower() == info || item.Universe.ToLower() == info || item.HeroName.ToLower() == info)
                {
                    listOfHeroFind.Add(item);
                }
            }

            if(listOfHeroFind.Count == 0)
            {
                return null;
            }
            else 
            {
                return listOfHeroFind;
            }
        }
    }
}