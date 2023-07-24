using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityLayer.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

       // [Authorize]
        [HttpGet]
        public List<GetAddressDTO> GetAllAddresses()
        {
            List<Address> addresses = _addressService.GetListAll();

            List<GetAddressDTO> addressDTOs = addresses.Select(address => new GetAddressDTO
            {
                Id = address.Id,
                AddressName = address.AddressName,
                CountryName = address.CountryName,
                CityName = address.CityName,
                TownName = address.TownName,
                DistrictName = address.DistrictName,
                PostCode = address.PostCode,
                AddressText = address.AddressText
            }).ToList();

            return addressDTOs;
        }

        //[Authorize]
        [HttpGet("get")]
        public Address GetAddress( int id)
        {
            var address = _addressService.GetElementById(id);

            if (address == null)
            {
                throw new Exception("NotFound");
            }

            return address;
        }

        //[Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAddress(GetAddressDTO dto)
        {

            if (ModelState.IsValid)
            {
                var addressToUpdate = _addressService.GetElementById(dto.Id);
                if (addressToUpdate == null)
                {
                    return NotFound();
                }

                addressToUpdate.AddressName = dto.AddressName;
                addressToUpdate.CountryName = dto.CountryName;
                addressToUpdate.CityName = dto.CityName;
                addressToUpdate.TownName = dto.TownName;
                addressToUpdate.DistrictName = dto.DistrictName;
                addressToUpdate.PostCode = dto.PostCode;
                addressToUpdate.AddressText = dto.AddressText;

                _addressService.Update(addressToUpdate);

                return Ok("Address successfully updated");
            }
            else
            {
                return BadRequest("Invalid data provided.");
            }
        }

        //[Authorize]
        [HttpPost("addaddress")]
        public async Task<ActionResult<DefaultAddressDTO>> AddAddress(DefaultAddressDTO address)
        {
            _addressService.Insert(new Address()
            {
                AddressName = address.AddressName,
                CountryName = address.CountryName,
                CityName = address.CityName,
                TownName = address.TownName,
                DistrictName = address.DistrictName,
                PostCode = address.PostCode,
                AddressText = address.AddressText,
            });

            return address;
        }

        //[Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAddress(int addressID)
        {
            var address = _addressService.GetElementById(addressID);
            if (address == null)
            {
                return NotFound();
            }

            _addressService.Delete(address);

            return Ok("Address deleted successfully");
        }

        
    }
}
