class Player { }
class Weapon { }
class UnitMover { }
class UnitProvider
{
    public IReadOnlyCollection<Unit> Units { get; private set; }
}