using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartBusinessAPI.Entities.Verification
{
    public class Data
    {
        [JsonProperty("national-id")]
        public bool NationalId { get; set; }

        [JsonProperty("proof-of-residency")]
        public bool ProofOfResidency { get; set; }
        public string birthDate { get; set; }
        public object curp { get; set; }
        public object fullName { get; set; }
        public string gender { get; set; }
        public string name { get; set; }
        public object nationality { get; set; }
        public object surname { get; set; }
        public string secondSurname { get; set; }
        public object documentNumber { get; set; }
        public object cde { get; set; }
        public object ne { get; set; }
        public string federalDistrict { get; set; }
        public object ocrNumber { get; set; }
        public string registrationYear { get; set; }
        public string emissionYear { get; set; }
        public bool? isPep { get; set; }
        public bool? registeredTaxPayer { get; set; }
        public int? age { get; set; }
        public int? ageThreshold { get; set; }
        public bool? underage { get; set; }
        public bool? duplicateSignup { get; set; }
        public List<string> relatedRecords { get; set; }
        public double? score { get; set; }
        public string nameSearched { get; set; }
        public string profileUrl { get; set; }
        public string searchedOn { get; set; }
        public int? searchId { get; set; }
        public Address address { get; set; }
        public EmissionDate emissionDate { get; set; }
        public DateOfBirth dateOfBirth { get; set; }
        public ExpirationDate expirationDate { get; set; }
        public DocumentType documentType { get; set; }
        public FirstName firstName { get; set; }
        public IssueCountry issueCountry { get; set; }
        public Optional2 optional2 { get; set; }
        public Sex sex { get; set; }
        public string facematchScore { get; set; }
        public List<Source> sources { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string regionCode { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public bool? safe { get; set; }
        public bool? ipRestrictionEnabled { get; set; }
        public bool? vpnDetectionEnabled { get; set; }
        public string platform { get; set; }
        public string selfiePhotoUrl { get; set; }
    }
}
