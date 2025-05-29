using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Revalb.Models;

public enum Zanrovi
{
    [Display(Name = "Pop")]
    Pop,
    [Display(Name = "Rock")]
    Rock,
    [Display(Name = "HipHop")]
    HipHop,
    [Display(Name = "Classical music")]
    KlasičnaMuzika,
    [Display(Name = "Electronic music")]
    ElektronskaMuzika,
    [Display(Name = "Country")]
    Country,
    [Display(Name = "Jazz")]
    Jazz,
    [Display(Name = "Blues")]
    Blues,
    [Display(Name = "R&B")]
    RnB,
    [Display(Name = "World")]
    World,
    [Display(Name = "Ambient")]
    Ambient,
    [Display(Name = "Soundtrack")]
    Soundtrack,
    [Display(Name = "Alternative")]
    Alternative
}