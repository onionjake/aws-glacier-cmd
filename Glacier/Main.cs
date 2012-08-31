using System;
using System.Collections.Generic;
using Amazon.Glacier;
using Amazon.Glacier.Transfer;
using Amazon.Runtime;
using Mono.Options;

namespace Glacier
{
  class ArchiveUploadHighLevel
  {
    string VaultName;
	ArchiveTransferManager manager;

	public ArchiveUploadHighLevel (string vaultName, Amazon.RegionEndpoint endpoint) {
			VaultName = vaultName;
			manager = new ArchiveTransferManager(endpoint);
	}
		
    public void upload(string fileName, string description)
    {
       try
      {
          // Upload an archive.
          string archiveId = manager.Upload(VaultName, description, fileName).ArchiveId;
          Console.WriteLine("Archive ID: (Copy and save this ID for the next step) : {0}", archiveId);
          Console.ReadKey();
      }
      catch (AmazonGlacierException e) { Console.WriteLine(e.Message); }
      catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
      catch (Exception e) { Console.WriteLine(e.Message); }
      Console.WriteLine("To continue, press Enter");
      Console.ReadKey();
    }
  }
}
namespace Glacier
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string vault = "";
			bool show_help = false;
			
			var p = new OptionSet () {
				{ "v|vault=", "the name of an existing {VAULT}.",
					v => vault = v },
				{ "h|help", "show this message and exit",
					v => show_help = v != null },
			};
			
			List<string> files;
			try {
				files = p.Parse (args);
			}
			catch (OptionException e) {
				Console.Write( "glacier: ");
				Console.WriteLine (e.Message);
				Console.WriteLine ("Try 'glacier --help'");
				return;
			}
			
			if (show_help) {
				ShowHelp (p);
				return;
			}
			var uploader = new ArchiveUploadHighLevel("Backup",Amazon.RegionEndpoint.USEast1);
			Console.WriteLine ("Vault: {0}", vault);
			foreach (string file in files) {
				Console.WriteLine ("Uploading {0}",file);
				uploader.upload(file,"Testing file upload");
			}
		}
		
		static void ShowHelp (OptionSet p)
		{
			Console.WriteLine("Usage: glacier [OPTIONS]+ file [file]+");
			p.WriteOptionDescriptions(Console.Out);
		}
	}
}
