﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrtBreakerClient
{
   public enum ResponseStatusCode
    {
        //..1xx http codes
        Failed = -1,

        //..1xx http codes
        Status200Continue = 100,
        Status101SwitchingProtocols = 101,
        Status102Processing = 102,
        Status103Checkpoint = 103,

        //..2xx http codes
        Status200Ok = 200,
        Status201Created = 201,
        Status202Accepted = 202,
        Status203NonAuthoritativeInformation = 203,
        Status204NoContent = 204,
        Status205ResetContent = 205,
        Status206PartialContent = 206,
        Status207MultiStatus = 207,
        Status208AlreadyReported = 208,

        //..3xx http codes
        Status300MultipleChoices = 300,
        Status301MovedPermanently = 301,
        Status302Found = 302,
        Status303SeeOther = 303,
        Status304NotModified = 304,
        Status305UseProxy = 305,
        Status306SwitchProxy = 306,
        Status307TemporaryRedirect = 307,
        Status308PermanentRedirect = 308,

        //..4xx http codes
        Status400BadRequest = 400,
        Status401Unauthorized = 401,
        Status402PaymentRequired = 402,
        Status403Forbidden = 403,
        Status404NotFound = 404,
        Status405MethodNotAllowed = 405,
        Status406NotAcceptable = 406,
        Status407ProxyAuthenticationRequired = 407,
        Status408RequestTimeout = 408,
        Status409Conflict = 409,
        Status410Gone = 410,
        Status411LengthRequired = 411,
        Status412PreconditionFailed = 412,
        Status413RequestEntityTooLarge = 413,
        Status414RequestUriTooLong = 414,
        Status415UnsupportedMediaType = 415,
        Status416RequestedRangeNotSatisfiable = 416,
        Status417ExpectationFailed = 417,
        Status418ImATeapot = 418,
        Status422UnprocessableEntity = 422,
        Status423Locked = 423,
        Status424FailedDependency = 424,
        Status425Unassigned = 425,
        Status426UpgradeRequired = 426,
        Status428PreconditionRequired = 428,
        Status429TooManyRequests = 429,
        Status431RequestHeaderFiledsTooLarge = 431,
        Status451UnavailableForLegalReasons = 451,

        //..5xx http codes
        Status500InternalServerError = 500,
        Status501NotImplemented = 501,
        Status502BadGateway = 502,
        Status503ServiceUnavailable = 503,
        Status504GatewayTimeout = 504,
        Status505HttpVersionNotSupported = 505,
        Status506VariantAlsoNegotiates = 506,
        Status507InsufficientStorage = 507,
        Status508LoopDetected = 508,
        Status509BandwidthLimitExceeded = 509,
        Status510NotExtended = 510,
        Status511NetworkAuthenticationRequired = 511,
        Status512NotUpdated = 512
    
}
}
