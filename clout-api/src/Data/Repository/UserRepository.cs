// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Repository.Base;

namespace clout_api.Data.Repository;

public class UserRepository : UserRepositoryBase
{
    public UserRepository(PostgresContext postgresContext) : base(postgresContext)
    {
    }
}
