internal class Program
{
    private static void Main(string[] args)
    {

    }

    private class Weapon
    {
        private const int MinBullets = 0;
        private const int BulletsPerShot = 1;
        private int _bullets;

        public bool CanShoot() => _bullets > MinBullets;

        public void Shoot() => _bullets -= BulletsPerShot;
    }
}