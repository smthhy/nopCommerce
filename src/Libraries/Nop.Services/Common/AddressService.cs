using System;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Core.Events;

namespace Nop.Services.Common
{
    /// <summary>
    /// Address service
    /// </summary>
    public partial class AddressService : IAddressService
    {
        #region Fields

        private readonly IRepository<Address> _addressRepository;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="addressRepository">Address repository</param>
        /// <param name="eventPublisher"></param>
        public AddressService(IRepository<Address> addressRepository,
            IEventPublisher eventPublisher)
        {
            _addressRepository = addressRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes an address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void DeleteAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            _addressRepository.Delete(address);

            //event notification
            _eventPublisher.EntityDeleted(address);
        }

        /// <summary>
        /// Gets an address by address identifier
        /// </summary>
        /// <param name="addressId">Address identifier</param>
        /// <returns>Address</returns>
        public virtual Address GetAddressById(int addressId)
        {
            if (addressId == 0)
                return null;

            var address = _addressRepository.GetById(addressId);
            return address;
        }

        /// <summary>
        /// Inserts an address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void InsertAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            
            address.CreatedOnUtc = DateTime.UtcNow;

            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;

            _addressRepository.Insert(address);

            //event notification
            _eventPublisher.EntityInserted(address);
        }

        /// <summary>
        /// Updates the address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void UpdateAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;

            _addressRepository.Update(address);

            //event notification
            _eventPublisher.EntityUpdated(address);
        }

        #endregion
    }
}