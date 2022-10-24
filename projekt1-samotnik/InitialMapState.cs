namespace samotnik;

public class InitialMapState {
	public readonly int[,] places;
	public readonly int[,] pawns;

	public InitialMapState(int[,] places, int[,] pawns) {
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
		new int[,] {
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 0, 0, 1, 1, 1, 0, 0 }
		},
		new int[,] {
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 1, 1, 1, 0, 1, 1, 1 },
			{ 1, 1, 1, 1, 1, 1, 1 },
			{ 0, 0, 1, 1, 1, 0, 0 },
			{ 0, 0, 1, 1, 1, 0, 0 }
		});
	
}
