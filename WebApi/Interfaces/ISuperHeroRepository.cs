using System;

namespace WebApi.Interfaces
{
    public interface ISuperHeroRepository : IGenericRepository <SuperHero>
    {
        List<SuperHero> GetAllInformation(string info);
    }
}