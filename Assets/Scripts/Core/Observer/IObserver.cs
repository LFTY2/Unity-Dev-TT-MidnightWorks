namespace Core.Observer
{
	public interface IObserver
	{
		void OnObjectChanged(Observable observable);
	}
}