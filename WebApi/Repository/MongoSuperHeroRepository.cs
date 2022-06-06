using WebApi;
using WebApi.Data;
using Microsoft.Extensions.Options;
using WebApi.Interfaces;
using MongoDB.Driver;

namespace WebApi.Repository;

public class MongoSuperHeroRepository : ISuperHeroRepository
{
    private readonly IMongoCollection<SuperHero> SuperHeroCollection;

    public MongoSuperHeroRepository(IOptions<SuperHeroMongoSetting> SuperHeroMongoSetting)
    {
        var mongoClient = new MongoClient(
            SuperHeroMongoSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            SuperHeroMongoSetting.Value.DatabaseName);

        this.SuperHeroCollection = mongoDatabase.GetCollection<SuperHero>(
            SuperHeroMongoSetting.Value.SuperHeroCollectionName);
    }

    /**************** Generic interface ****************/
    /**************************************************/
    
    public List<SuperHero> Read()
    {
        return this.SuperHeroCollection.Find(_ => true).ToList();
    }

    public SuperHero Update(SuperHero sp)
    {
        SuperHero superHero = this.SuperHeroCollection.Find(x => x.Id == sp.Id).FirstOrDefault();
        
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
            
            return sp;
        }

        return null;
    }

    public SuperHero ReadOne(int id)
    {
        var tempItem = this.SuperHeroCollection.Find(x => x.Id == id).FirstOrDefault();

        if(tempItem == null)
        {
            return null;
        }

        return this.SuperHeroCollection.Find(x => x.Id == id).FirstOrDefault();
    }

    public SuperHero Delete(int id)
    {
        var tempItem = this.SuperHeroCollection.Find(x => x.Id == id).FirstOrDefault();

        if(tempItem == null)
        {
            return null;
        }

        this.SuperHeroCollection.DeleteOne(x => x.Id == id);
        SuperHero sp = new SuperHero();
        return sp;
    }

    public SuperHero Create(SuperHero sp)
    {
        List<SuperHero> ListOfHeroInDataBase = this.SuperHeroCollection.Find(_ => true).ToList();

        foreach (var item in ListOfHeroInDataBase)
        {
            if(sp.Id == item.Id)
            {
                return null;
            }
            
            if(sp.HeroName.ToLower() == item.HeroName.ToLower())
            {
                return null;
            }
        }

        this.SuperHeroCollection.InsertOne(sp);
        return sp;
    }

    
    /**************** SuperHero interface ****************/
    /****************************************************/

    public List<SuperHero> GetAllInformation(string info)
    {
        info = info.ToLower();
        List<SuperHero> ListOfHeroInDataBase = this.SuperHeroCollection.Find(_ => true).ToList();
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

        return listOfHeroFind;
    }


}