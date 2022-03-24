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
	public bool LogDoorLocked_NoFile_NewFileCreated()
	{
		_uut.LogDoorLocked(0);
		
		
	}
}