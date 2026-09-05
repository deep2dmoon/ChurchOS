using producer.model;

namespace htmos;

public record class AccountResponse(
    string Name,
    int RoleID,
    string Branch,
    string Email,
   IEnumerable<object> Members
);