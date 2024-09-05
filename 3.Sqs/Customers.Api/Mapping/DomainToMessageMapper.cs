using Customers.Api.Contracts.Messages;
using Customers.Api.Domain;

namespace Customers.Api.Mapping;
public static class DomainToMessageMapper
{
   public static CustomerCreated ToCustomerCreatedMessage(this Customer customer)
   {
      return new CustomerCreated
      {
         Id = customer.Id,
         DateOfBirth = customer.DateOfBirth,
         Email = customer.Email,
         FullName = customer.FullName,
         GitHubUsername = customer.GitHubUsername,
      };
   }

   public static CustomerUpdated ToCustomerUpdatedMessage(this Customer customer)
   {
      return new CustomerUpdated
      {
         Id = customer.Id,
         DateOfBirth = customer.DateOfBirth,
         Email = customer.Email,
         FullName = customer.FullName,
         GitHubUsername = customer.GitHubUsername,
      };
   }
}

