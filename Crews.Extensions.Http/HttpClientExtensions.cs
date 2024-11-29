namespace Crews.Extensions.Http;

/// <summary>
/// Contains extension methods useful for interacting with <see cref="HttpClient"/> instances.
/// </summary>
public static class HttpClientExtensions
{
	/// <summary>
	/// Sets the <see cref="HttpClient.BaseAddress"/> property for <paramref name="client"/> while avoiding the side
	/// effects of <see cref="Uri"/> permutations detailed <a href="https://stackoverflow.com/a/23438417">here</a>.
	/// </summary>
	/// <param name="client">The <see cref="HttpClient"/> instance.</param>
	/// <param name="baseAddress">
	/// The <see cref="Uri"/> instance to use as the <see cref="HttpClient.BaseAddress"/>.
	/// </param>
	/// <remarks>
	/// This extension method only fills half of a larger pitfall with the <see cref="HttpClient.BaseAddress"/> feature:
	/// When calling HTTP methods like <see cref="HttpClient.GetAsync(Uri?)"/> with a relative <see cref="Uri"/> instance,
	/// you <em>must not</em> include a leading slash in the relative path.
	/// </remarks>
	public static void SafelySetBaseAddress(this HttpClient client, Uri baseAddress)
		=> client.BaseAddress = baseAddress.EnsureTrailingSlash();
}
