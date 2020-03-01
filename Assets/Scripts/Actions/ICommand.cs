public interface ICommand
{
  void Execute();
  void Dispose();
  void SetDestination(int cellID);
}
