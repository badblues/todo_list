namespace TodoList.WebApi.Dtos;

public class Response<T> {

  public T? Data {get; set;}

  public string Error {get; set;} = String.Empty;

}