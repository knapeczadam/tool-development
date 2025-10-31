namespace ImageEffectEditor
{
    interface ICommand
    {
        void Execute();
        void Undo();
    }
}
