using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<PlayerNetworkData> networkState = new NetworkVariable<PlayerNetworkData>(writePerm: NetworkVariableWritePermission.Owner);
    private Vector3 velocity;
    private float rotVelcoity;
    private float interpolTime = 0.1f;
    void Update()
    {
        if (IsOwner)
        {
            networkState.Value = new PlayerNetworkData()
            {
                Position = transform.position,
                Rotation = transform.rotation.eulerAngles,
            };
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, networkState.Value.Position, ref velocity, interpolTime);
            transform.rotation = Quaternion.Euler(0, Mathf.SmoothDamp(transform.rotation.eulerAngles.y, networkState.Value.Rotation.y, ref rotVelcoity, interpolTime), 0);
        }
    }

    struct PlayerNetworkData: INetworkSerializable
    {
        private float xPos, zPos;
        private float yRot;

        internal Vector3 Position
        {
            get => new Vector3(xPos, 0.0f, zPos);

            set
            {
                xPos = value.x;
                zPos = value.z;
            }
        }

        internal Vector3 Rotation
        {
            get => new Vector3(0.0f, yRot, 0.0f);
            set
            {
                yRot = value.y;
            }
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref xPos);
            serializer.SerializeValue(ref zPos);
            serializer.SerializeValue(ref yRot);
        }
    }
}
