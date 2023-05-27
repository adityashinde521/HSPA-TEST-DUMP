﻿using HSPA_TEST.BLL.DTOs;
using HSPA_TEST.DAL.Data;
using HSPA_TEST.DAL.Models;
using HSPA_TEST.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HSPA_TEST.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        private readonly DataContext dbContext; //This line declares a private field named dbContext of type DataContext.It is used to access the data in the database.
        private readonly ICityRepository cityRepository;

        public CityController(DataContext dbContext, ICityRepository cityRepository) //This is the constructor for the CityController class.
                                                     //It takes a parameter of type DataContext and assigns it to the dbContext field.
                                                     //This is known as dependency injection, where the DataContext is provided to the controller when it is created.

        {
            this.dbContext = dbContext;
            this.cityRepository = cityRepository;
        }

        //GET ALL Cities
        //https://localhost:portno/api/City
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database - Domain Model file
            var citiesDomain = dbContext.Cities.ToList();

            //Map Domain Models to Dtos
            var citiesDtos = new List<CityDto>();  //comes from DTO folder

            foreach (var cityDomain in citiesDomain) 
            {
                citiesDtos.Add(new CityDto()
                {
                    Id = cityDomain.Id,
                    Name = cityDomain.Name,
                    Country = cityDomain.Country,
                });

            }
            //Return Dtos
            return Ok(citiesDtos); //citiesDomain
        }


        //GET Single CITY (by ID)
        //https://localhost:port/api/City/id
        [HttpGet]
        [Route("{Id:Guid}")]    
        public IActionResult GetById([FromRoute] Guid Id)
        {
            //Get city domain moel from DB
            //var city = dbContext.Cities.Find(Id);
            //M-2 : 
            var cityDomain = dbContext.Cities.FirstOrDefault( x => x.Id == Id);

            if (cityDomain == null)
            {
                return NotFound();
            }

            //Mapping/Convert City Domain Model to City DTOs
            var cityDto = new CityDto
            {
                Id = cityDomain.Id,
                Name = cityDomain.Name,
                Country = cityDomain.Country,
            };

            //Return Dtos
            return Ok(cityDto);
        }


        //Post to create new City
        //https://localhost:port/api/City/
        [HttpPost]
        public IActionResult Create([FromBody] CityDto cityDto)
        {
            // Reverse >> Map DTO to domain model
            var cityDomainModel = new City
            {
                Name = addCityRequestDto.Name,
                Country = addCityRequestDto.Country
            };

            // Save the city to the database or perform any necessary operations
            dbContext.Cities.Add(citydomainmodel);
            dbContext.SaveChanges();

            //Remapping domain model to Dtos

            var CityDtoRemapped = new CityDto
            {
                Id = cityDomainModel.Id,
                Name = cityDomainModel.Name,
                Country = cityDomainModel.Country
            };

            // Return a successful response with the created city
            return CreatedAtAction(nameof(GetById), new { Id = cityDomainModel.Id }, CityDtoRemapped);
        }

        
        //  Update an existing city in the Cities table.
        //PUT : https://localhost:port/api/city/{id}
         [HttpPut]
         [Route("{Id:Guid}")]
         public IActionResult UpdateCity(Guid id, [FromBody] CityDto cityDto)
        {
            var city = dbContext.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            //Map Dto to Domain Model
            cityDomainModel.Name = cityChangesRequestDto.Name;
            cityDomainModel.Country = cityChangesRequestDto.Country;

            await dbContext.SaveChangesAsync();

            dbContext.SaveChanges();

            //Convert Domain Model to DTo
            var cityDtoRemapped = new CityDto
            {
                Id = cityDomainModel.Id,
                Name = cityDomainModel.Name,
                Country = cityDomainModel.Country
            };

            return Ok(cityDtoRemapped);
        }


        //Delete - Delete a city from the Cities table.
        [HttpDelete("{id}")]
        public IActionResult DeleteCity(Guid id)
        {
            var city = dbContext.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            dbContext.Cities.Remove(city);
            dbContext.SaveChanges();

            return NoContent();
        }

    }
}












/* Manual Injection values via hardcoded list for testing api
           [HttpGet]
           public IActionResult GetAllCities()
           {
           var cities = new List<City>
           {
           new City
           {
           Id = Guid.NewGuid(),
           Name = "Nashik",
           Country = "India",
           },

           new City
           {
           Id = Guid.NewGuid(),
           Name = "Mumbai",
           Country = "India",
           },
           new City
           {
           Id = Guid.NewGuid(),
           Name = "Bangalore",
           Country = "India",
           },
           };
           return Ok(cities);
           }*/