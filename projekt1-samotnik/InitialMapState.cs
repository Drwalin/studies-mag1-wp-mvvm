namespace samotnik;

public class InitialMapState {
	public readonly int[,] places;
	public readonly int[,] pawns;
	public readonly string name;

	public InitialMapState(string name, int[,] places, int[,] pawns) {
		this.name = name;
		this.places = places;
		this.pawns = pawns;
	}

	public int GetWidth() {
		return places.GetLength(0);
	}

	public int GetHeight() {
		return places.GetLength(1);
	}

	public static readonly InitialMapState defaultMap = new InitialMapState(
		"Normal board",
		new[,] {
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 0, 0, 1, 1, 1, 0, 0 }
		},
		new[,] {
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 0, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 0, 0, 1, 1, 1, 0, 0 }
		});
	
	public static readonly InitialMapState easyMap = new InitialMapState(
		"Easy board",
		new[,] {
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 0, 1, 1, 1, 1, 1, 0 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 0, 1, 1, 1, 1, 1, 0 },
			{ 0, 0, 1, 1, 1, 0, 0 }
		},
		new[,] {
			{ 0, 0, 1, 1, 0, 0, 0 },
			{ 0, 0, 1, 1, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 1, 1, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 1, 1, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0 }
		});

	public static readonly InitialMapState[] internalMaps = { defaultMap, easyMap };
}
