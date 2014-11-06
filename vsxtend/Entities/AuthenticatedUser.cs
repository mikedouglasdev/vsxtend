using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsxtend.Entities
{

    public class SchemaClassName
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Description
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Domain
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Account
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class DN
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Mail
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class SpecialType
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class ComplianceValidated
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Properties
{
    public SchemaClassName SchemaClassName { get; set; }
    public Description Description { get; set; }
    public Domain Domain { get; set; }
    public Account Account { get; set; }
    public DN DN { get; set; }
    public Mail Mail { get; set; }
    public SpecialType SpecialType { get; set; }
    public ComplianceValidated ComplianceValidated { get; set; }
}

public class AuthenticatedUser
{
    public string id { get; set; }
    public string descriptor { get; set; }
    public string providerDisplayName { get; set; }
    public string customDisplayName { get; set; }
    public bool isActive { get; set; }
    public List<object> members { get; set; }
    public List<object> memberOf { get; set; }
    public Properties properties { get; set; }
}

public class SchemaClassName2
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Description2
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Domain2
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Account2
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class DN2
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Mail2
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class SpecialType2
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class ComplianceValidated2
{
    public string __invalid_name__type { get; set; }
    public string __invalid_name__value { get; set; }
}

public class Properties2
{
    public SchemaClassName2 SchemaClassName { get; set; }
    public Description2 Description { get; set; }
    public Domain2 Domain { get; set; }
    public Account2 Account { get; set; }
    public DN2 DN { get; set; }
    public Mail2 Mail { get; set; }
    public SpecialType2 SpecialType { get; set; }
    public ComplianceValidated2 ComplianceValidated { get; set; }
}

public class AuthorizedUser
{
    public string id { get; set; }
    public string descriptor { get; set; }
    public string providerDisplayName { get; set; }
    public string customDisplayName { get; set; }
    public bool isActive { get; set; }
    public List<object> members { get; set; }
    public List<object> memberOf { get; set; }
    public Properties2 properties { get; set; }
}

public class LocationServiceData
{
    public string serviceOwner { get; set; }
    public string defaultAccessMappingMoniker { get; set; }
    public int lastChangeId { get; set; }
}

public class AuthenticatedUserResult
{
    public AuthenticatedUser authenticatedUser { get; set; }
    public AuthorizedUser authorizedUser { get; set; }
    public string instanceId { get; set; }
    public LocationServiceData locationServiceData { get; set; }
    public string webApplicationRelativeDirectory { get; set; }
}
}