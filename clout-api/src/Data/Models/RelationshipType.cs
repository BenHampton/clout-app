// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace clout_api.Data.Models;

public enum RelationshipType
{
    FRIEND = 1,
    UNFRIEND = 2,
    REQUEST = 3,
    PENDING_INCOMING = 4,
    PENDING_OUTGOING = 5,
    APPROVED_REQUEST = 6,
    REJECT = 7,
    BLOCK = 8,
    BLOCKED = 9,
}
