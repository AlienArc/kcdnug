namespace kcdnug.Mobile.Repositories
{
	public class RepositoryResult<T>
	{
		public RepositoryResultStatus Status { get; set; }
		public T Data { get; set; }
	}

}
