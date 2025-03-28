﻿using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Buyer;

public class AddAddressRequest
{
    [StringLength(200, ErrorMessage = "Thông tin đường không được dài quá 200 kí tự.")]
    public string Street { get; set; } = string.Empty;
    [StringLength(200, ErrorMessage = "Thông tin thành phố không được dài quá 200 kí tự.")]
    public string City { get; set; } = string.Empty;
    [StringLength(200, ErrorMessage = "Thông tin tỉnh không được dài quá 200 kí tự.")]
    public string State { get; set; } = string.Empty;
    [StringLength(200, ErrorMessage = "Thông tin đất nước không được dài quá 200 kí tự.")]
    public string Country { get; set; } = string.Empty;
}
