﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDSignWebApp.ViewModels
{
    public class BookingModel
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public int SignRenterId { get; set; }

        // List of Lines
        public List<BookingLineModel> Lines { get; set; }

        public BookingModel()
        {
            Lines = new List<BookingLineModel>();
        }

        public BookingModel(int id, DateTime bookingDate, string bookingStatus, decimal totalPrice, int signRenterId)
            : this()
        {
            Id = id;
            BookingDate = bookingDate;
            BookingStatus = bookingStatus;
            TotalPrice = totalPrice;
            SignRenterId = signRenterId;
        }
    }
}
