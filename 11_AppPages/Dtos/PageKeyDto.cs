﻿namespace Dtos;

public class PageKeyDto
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public string ValueAr { get; set; }
    public string ValueEn { get; set; }
    public Guid PageId { get; set; }
}
