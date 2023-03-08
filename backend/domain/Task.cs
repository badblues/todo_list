namespace domain;

using System;

public class Task
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public bool Completed { get; set; }
  public String Title { get; set; }
  public String Details { get; set; }
  public DateTime CreationDate { get; set; }
  public DateTime EditDate { get; set; }

}
