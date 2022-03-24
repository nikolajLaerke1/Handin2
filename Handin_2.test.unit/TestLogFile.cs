using System.IO;
using Handin2;
using NUnit.Framework;

namespace Handin_2.test.unit;

[TestFixture]
public class TestLogFile
{
	private LogFile _uut;
	private readonly string testPath = "testLog.txt";
	
	[SetUp]
	public void SetUp()
	{
		if(File.Exists(testPath))
		{
			File.Delete(testPath);
		}
		_uut = new LogFile(testPath);
	}

	[Test]
	public void LogDoorLocked_NoFile_NewFileCreated()
	{
		_uut.LogDoorLocked(0);

		Assert.True(File.Exists(testPath));

	}
	
	[Test]
	public void LogDoorUnLocked_NoFile_NewFileCreated()
	{
		_uut.LogDoorUnlocked(0);

		Assert.True(File.Exists(testPath));

	}

	[Test]
	public void LogFileCtor_NoObjectExists_ObjectCreated()
	{
		LogFile uut2 = new();
		
		Assert.IsNotNull(uut2);
	}
}