namespace Crews.Extensions.Http.Tests;

public class HttpClientExtensionsTests
{
	[Theory(DisplayName = "SafelySetBaseAddress correctly sets base client address")]
	[InlineData("http://a.bc")]
	[InlineData("http://a.bc/")]
	[InlineData("http://a.bc////")]
	public void SafelySetBaseAddress_CorrectlySetsAddress(string uriString)
	{
		HttpClient subject = new();
		Uri expected = new("http://a.bc/");
		subject.SafelySetBaseAddress(new(uriString));
		Uri? actual = subject.BaseAddress;

		Assert.Equal(expected, actual);
	}
}
