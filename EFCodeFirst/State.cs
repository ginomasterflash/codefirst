using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCodeFirst
{
    //public class State
    //{
    //    private State(string value) { Value = value; }

    //    //[Key]
    //    public string Value { get; set; }

    //    public static State Active { get { return new State("Active"); } }
    //    public static State Inactive { get { return new State("Inactive"); } }
    //    public static State Deleted { get { return new State("Deleted"); } }
    //}
    public enum State
    {
        Active, Inactive, Deleted
    }
    //public sealed class State
    //{

    //    private State() { }

    //    public static readonly string Active = "Active";
    //    public static readonly string Inactive = "Inactive";
    //    public static readonly string Deleted = "Deleted";
    //}
}
