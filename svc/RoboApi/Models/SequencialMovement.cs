namespace RoboApi.Models;

public class SequencialMovement<T>
{
    private readonly T[] _moveSet;
    private T _currentPosition;

    public SequencialMovement(T[] moveSet, T startPosition)
    {
        _moveSet = moveSet;
        if (!moveSet.Contains(startPosition))
            throw new ArgumentException("Start position must be in the moveset");

        _currentPosition = startPosition;
    }

    public T CurrentPosition => _currentPosition;

    public SequencialMovement<T> MoveTo(T position)
    {
        if (!ValidateMove(position))
            throw new InvalidOperationException("Invalid move");

        _currentPosition = position;

        return this;
    }

    public bool ValidateMove(T position)
    {
        if (!_moveSet.Contains(position))
            return false;

        var currentIndex = Array.IndexOf(_moveSet, _currentPosition);
        var targetIndex = Array.IndexOf(_moveSet, position);

        return Math.Abs(currentIndex - targetIndex) <= 1;
    }
}