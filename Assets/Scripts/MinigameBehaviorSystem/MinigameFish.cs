using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinigameFish : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _topLinePoint;

    [SerializeField] private float _catchProgressDelta;

    [SerializeField] private float _secToChangeMoveDirection = 0.25f;

    [SerializeField] private float _speedDelta = 5;


    [SerializeField] private float _rotationSpreadMin = 90;
    [SerializeField] private float _rotationSpreadMax = 270;
    [SerializeField] private float _speed = 5f;
    private float _movementAngle; // угол направления

    private Vector2 _movementDirection; // направление движения
    private float _rotationSpeed = 0.5f;

    private float _speedDecreaseTimer;

    private Transform _transform;


    public void MinigameStart(int fishRarenesIndex)
    {
        _speedDecreaseTimer = 0;
        _transform = transform;
        _speed = (fishRarenesIndex + 1);
        _rotationSpeed = _speed / 2;
        SetRandomDirection(); //задаем начальное направление
        StartCoroutine(FishDirectionCoroutine()); //включаем корутину направления
    }

    public void MinigameEnd()
    {
        StopAllCoroutines(); //выключаем корутину направления

        _transform.position = new Vector3(0, -1, 0);
        _transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void FishMovementUpdate()
    {
        RotateFish(_movementAngle); //меняем отображение спрайта относительно движения
        _rb.AddForce(_movementDirection * _speed * _speedDelta); //двигаем рыбку
        _speedDecreaseTimer += Time.deltaTime;

        if (_speedDecreaseTimer > 10f && _speed >= 2)
        {
            _speed -= 1;
            _speedDecreaseTimer = 0;
        }
    }

    public void Pull(float catchForce)
    {
        var targetPosition = _topLinePoint.position;

        Vector3 direction = targetPosition - _transform.position;

        // Вычисляем расстояние между текущим и целевым объектом
        float distance = direction.magnitude;

        // Проверяем, достиг ли текущий объект целевого объекта
        if (distance > 0.1f)
        {
            // Нормализуем направление движения
            direction.Normalize();

            // Вычисляем скорость движения с учетом времени и заданной скорости
            float movementSpeed = catchForce * _catchProgressDelta * Time.deltaTime;

            // Двигаем текущий объект в направлении целевого объекта с учетом скорости
            _transform.position += direction * movementSpeed;
        }
    }

    IEnumerator FishDirectionCoroutine() //корутина для изменения направления движения
    {
        while (true)
        {
            SetRandomDirection();

            yield return
                new WaitForSeconds(Random.Range(_secToChangeMoveDirection - 0.1f,
                    _secToChangeMoveDirection + 0.1f)); // ждем заданное время с погрешностью для реализма
        }
    }

    private void SetRandomDirection() // генерация случайного направления
    {
        float newMovementAngle = Random.Range(_rotationSpreadMin, _rotationSpreadMax); //рандомное значение угла


        _movementAngle = newMovementAngle;


        Vector2 direction = new Vector2(Mathf.Sin(_movementAngle * Mathf.Deg2Rad),
            Mathf.Cos(_movementAngle * Mathf.Deg2Rad)); //создаем вектор направления

        _movementDirection = direction; //возвращаем вектор
    }


    private void RotateFish(float movementAngle)
    {
        // Получаем текущий угол поворота объекта по оси Z
        float currentAngle = _transform.eulerAngles.z;


        // Целевой угол поворота
        float targetAngle = -movementAngle;

        // Интерполяция между текущим и целевым углом
        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * _rotationSpeed);

        // держим обьект в рамках заданного угла, чтобы рыбка плыла только "вниз"

        if (newAngle < _rotationSpreadMin)
        {
            newAngle = _rotationSpreadMin;
        }

        if (newAngle > _rotationSpreadMax)
        {
            newAngle = _rotationSpreadMax;
        }

        // Применяем новый угол поворота к объекту
        _transform.rotation = Quaternion.Euler(0, 0, newAngle);

        // Отражаем спрайт по горизонтали если нужно
        _spriteRenderer.flipX = newAngle < 180;
    }
}