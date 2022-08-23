# Structure

```
# PAX (magic: `PAX_`)
## Dat1List
## DPX (magic: 0x82) ;PaxHeader2
### DPDList ;Dat2List
#### DPD (magic: 0x96) ;PaxHeader3
##### EffectsGroupList ;Dat31List
###### EffectsGroup = Matrix1, Matrix2, Position, Rotation, Scaling, ...
###### Effects ;Dat31AList
####### Effect = OffsetNext, ...
####### EffectCommandList ;Dat31BList
######## EffectCommand = Command, ParamLength, ParamsCount, OffsetParameters, Offset2, Parameter, Parameters
##### Dat32List
###### Dat32 = Bitmap
##### Dat33List
###### Dat33 = Currently unknown
##### Dat34List
###### Dat34 = Currently unknown
##### Dat35List
###### Dat35 = Currently unknown
```

