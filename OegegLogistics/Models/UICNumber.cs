using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OegegLogistics.Models;

public record UICNumber(List<UICSelfCheckSegment> UICSegments);

public abstract record UICNumberSegment(string description);
public record UICSelfCheckSegment([Range(0,9)]uint Number) : UICNumberSegment("Self check number");
public record UICSerialNumberSegment([Range(100,999)]uint Number, string description) : UICNumberSegment(description);