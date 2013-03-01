
namespace MesserSmash.Commands {
    public interface ICommand {
        string Name { get; }
        void execute();
    }
}
